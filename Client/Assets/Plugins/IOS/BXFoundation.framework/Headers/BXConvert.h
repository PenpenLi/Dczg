
#import <Foundation/Foundation.h>

@interface BXConvert : NSObject

/*
 * lower case md5
 */
+ (NSString*)md5Str:(NSString*)str;

/*
 * url encoder
 */
+ (NSString*)urlEncode:(NSString*)str;

/*
 * convert between NSData and NSString
 */
+ (NSString*)stringFromDate:(NSDate*)date format:(NSString*)format;
+ (NSDate*)dateFromString:(NSString*)date format:(NSString*)format;

/*
 * simple encrypt and decrypt
 */
+ (NSString*)stringEncrypt:(NSString*)str;
+ (NSString*)stringDecrypt:(NSString*)str;

/*
 * 3DES
 */
+ (NSString*)stringEncrypt3DES:(NSString*)str key:(NSString*)key;
+ (NSString*)stringDecrypt3DES:(NSString*)str key:(NSString*)key;

/*
 * AES
 */
+ (NSString*)stringEncryptAES:(NSString*)str key:(NSString*)key;
+ (NSString*)stringDecryptAES:(NSString*)str key:(NSString*)key;
+ (NSData*)dataEncryptAES:(NSData*)data key:(NSString*)key;
+ (NSData*)dataDecryptAES:(NSData*)data key:(NSString*)key;


@end
