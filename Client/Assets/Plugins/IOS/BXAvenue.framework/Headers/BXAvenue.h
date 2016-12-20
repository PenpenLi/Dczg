//
//  BXAvene.h
//
#import <Foundation/Foundation.h>

#define BX_AVENUE_VERSION   @"1.0.0.1"
#define BX_AVENUE_MESSAGE_MAXLEN    60000

@interface BXAvenue : NSObject

+ (void)addStrValue:(NSMutableDictionary*)dataDic value:(NSString*)value tag:(NSInteger)tag;
+ (void)addIntValue:(NSMutableDictionary*)dataDic value:(NSInteger)value tag:(NSInteger)tag;
+ (void)addByteValue:(NSMutableDictionary*)dataDic value:(NSData*)value tag:(NSInteger)tag;
+ (void)addArrayValue:(NSMutableDictionary*)dataDic value:(NSArray*)value tag:(NSInteger)tag;

+ (void)dumpData:(NSData*)data title:(NSString*)title;

+ (BOOL)needBuffer:(NSData*)nowBuffer newData:(NSData*)newData;
+ (NSUInteger)firstDataLength:(NSData*)data;

+ (NSString*)getStr:(NSDictionary*)dataDic key:(NSInteger)key;
+ (NSInteger)getInt:(NSDictionary*)dataDic key:(NSInteger)key;
+ (NSData*)getByte:(NSDictionary*)dataDic key:(NSInteger)key;
+ (NSArray*)getStrArray:(NSDictionary*)dataDic key:(NSInteger)key;
+ (NSArray*)getIntArray:(NSDictionary*)dataDic key:(NSInteger)key;


+ (NSData*)getMediaData:(NSArray*)message;

+ (NSData*)encryptAvenuePackage:(NSData*)avenuePackage key:(NSString*)key;
+ (NSData*)decryptAvenuePackage:(NSData*)avenuePackage key:(NSString*)key;

@end
