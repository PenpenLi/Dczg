using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Hexagon
{
    public class Tile
    {

        //   8   Height
        //   3   Edge    Weight
        //   3   Actor   Weight
        //   1   HasSearch
        //   1   Wakable

        public static ushort HeightMask = 0xff;				//< 0b 0000 0000 1111 1111

        public static ushort WeightMask = 0x3f00;			//< 0b 0011 1111 0000 0000
		public static ushort WeightShift = 0x8;

        public static ushort ActorWeightMask = 0x3800;		//< 0b 0011 1000 0000 0000
        public static ushort ActorWeightStep = 0x800;
		public static ushort ActorWeightShift = 0xb;

        public static ushort EdgeWeightMask = 0x700;		//< 0b 0000 0111 0000 0000
        public static ushort EdgeWeightStep = 0x100;
		public static ushort EdgeWeightShift = 0x8;

        public static ushort HasSearch = 0x4000;
        public static ushort Walkable = 0x8000;
        //public static ushort    HasPlayer   =   0x8000;
        List<ushort[]> layerData = new List<ushort[]>();
        public int GetLayerCount()
        {
            return layerData.Count;
        }
        //ushort[] data = new ushort[16 * 16];
        public bool Insert(int x, int z, uint height)
        {
            int idx = z * Manager.TileSize + x;
            for (int i = 0; i < layerData.Count; i++)
            {
                if (layerData[i] == null)
                {
                    layerData[i] = new ushort[Manager.TileSize * Manager.TileSize];
                }
                ushort val = layerData[i][idx];

                if (val == 0)
                {
                    layerData[i][idx] = (ushort)(height | Walkable);
                    return true; ;
                }
                else if (height == (0xff & val))
                {
                    return true;
                }

            }

            ushort[] newLayer = new ushort[Manager.TileSize * Manager.TileSize];
            newLayer[idx] = (ushort)(height | Walkable);
            layerData.Add(newLayer);
            return true;
        }
        public bool isWalkable(int x, int z, int y)
        {
            if (y >= layerData.Count)
            {
                return false;
            }
            int idx = z * Manager.TileSize + x;
            ushort by = layerData[y][idx];
            if (by == 0)
            {
                return false;
            }
            if ((by & Walkable) == 0)
            {
                return false;
            }
            //if ((by & HasPlayer) != 0)
            //{
            //    return false;
            // }
            return true;
        }
        public bool isSearched(int x, int z, int y)
        {
            if (y >= layerData.Count)
            {
                return true;
            }
            int idx = z * Manager.TileSize + x;

            ushort by = layerData[y][idx];
            return (by & HasSearch) != 0;
        }
        public void AddSearchMark(int x, int z, int y)
        {
            if (y >= layerData.Count)
            {
                return;
            }
            int idx = z * Manager.TileSize + x;
            ushort by = layerData[y][idx];
            by |= HasSearch;
            layerData[y][idx] = by;
        }
        public void RemoveSearchMark(int x, int z, int y)
        {
            if (y >= layerData.Count)
            {
                return;
            }
            int idx = z * Manager.TileSize + x;
            ushort by = layerData[y][idx];
            int val = by & (~HasSearch);
            layerData[y][idx] = (ushort)val;
        }
        public ushort GetHeight(int x, int z, int y)
        {
            if (y >= layerData.Count)
            {
                return 0;
            }
            int idx = z * Manager.TileSize + x;
            return layerData[y][idx];
        }
        public void SetHeight(int x, int z, int y, ushort height)
        {
            if (y >= layerData.Count)
            {
                return;
            }
            int idx = z * Manager.TileSize + x;
            layerData[y][idx] = height;
        }
        /*
        public void CompareSwap(Tile high)
        {
            for (int z = 0; z < Manager.TileSize; z++)
            {
                for (int x = 0; x < Manager.TileSize; x++)
                {
                    ushort lowData = GetHeight(x, z);
                    ushort highData = high.GetHeight(x, z);
                    if (lowData == 0)
                    {
                        SetHeight(x, z, highData);
                        continue;
                    }
                    
                    if (highData == 0)
                    {
                        continue;
                    }
                    if ((highData & Tile.HeightMask) < (lowData & Tile.HeightMask))
                    {
                        high.SetHeight(x, z, lowData);
                        SetHeight(x, z, highData);
                    }
                }
            }
        }
        */
        public void DebugRender(int x, int z, GameObject parent, float fXStep, float fZStep, float fYStep)
        {
            for (int layer = 0; layer < layerData.Count; layer++)
            {
                GameObject tile = new GameObject();
                tile.transform.parent = parent.transform;
                tile.transform.localPosition = new Vector3(x * fXStep, 0, z * fZStep) * Manager.TileSize;
                tile.transform.localRotation = Quaternion.identity;
                tile.name = x + "_" + z + "_" + layer;

                MeshFilter mf = tile.AddComponent<MeshFilter>();
                MeshRenderer r = tile.AddComponent<MeshRenderer>();

                Vector3[] v = new Vector3[Manager.TileSize * Manager.TileSize];
                for (int zz = 0; zz < Manager.TileSize; zz++)
                {
                    for (int xx = 0; xx < Manager.TileSize; xx++)
                    {
                        float xoffset = 0.0f;
                        if ((zz & 1) == 1)
                        {
                            xoffset = fXStep * 0.5f;
                        }
                        ushort d = layerData[layer][zz * Manager.TileSize + xx];
                        float h = -10.0f;
                        if (d != 0)
                        {
                            h = (d & 0xff) * fYStep;
                        }

                        v[zz * Manager.TileSize + xx] = new Vector3(xx * fXStep + xoffset, h, zz * fZStep);//

                    }
                }

                int quadCount = (Manager.TileSize - 1) * (Manager.TileSize - 1);// *6;
                int[] f = new int[quadCount * 6];
                int Quad = 0;
                for (int zz = 0; zz < Manager.TileSize - 1; zz++)
                {
                    int oddnumber = zz & 1;
                    for (int xx = 0; xx < Manager.TileSize - 1; xx++)
                    {
                        if (oddnumber == 1)
                        {
                            f[Quad * 6] = (zz + 1) * Manager.TileSize + xx;
                            f[Quad * 6 + 1] = (zz + 1) * Manager.TileSize + xx + 1;
                            f[Quad * 6 + 2] = zz * Manager.TileSize + xx;
                            f[Quad * 6 + 3] = (zz + 1) * Manager.TileSize + xx + 1;
                            f[Quad * 6 + 4] = zz * Manager.TileSize + xx + 1;
                            f[Quad * 6 + 5] = zz * Manager.TileSize + xx;
                        }
                        else
                        {
                            f[Quad * 6] = (zz + 1) * Manager.TileSize + xx;
                            f[Quad * 6 + 1] = zz * Manager.TileSize + xx + 1;
                            f[Quad * 6 + 2] = zz * Manager.TileSize + xx;
                            f[Quad * 6 + 3] = (zz + 1) * Manager.TileSize + xx + 1;
                            f[Quad * 6 + 4] = zz * Manager.TileSize + xx + 1;
                            f[Quad * 6 + 5] = (zz + 1) * Manager.TileSize + xx;
                        }
                        Quad++;
                    }
                }
                mf.mesh.vertices = v;
                mf.mesh.triangles = f;
            }
        }
    }
}
