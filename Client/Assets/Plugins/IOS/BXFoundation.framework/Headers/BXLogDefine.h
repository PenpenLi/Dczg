#import<Foundation/Foundation.h>

#define _BXLog(s, ...)  __BXLog(s, ##__VA_ARGS__)

#define _BXLogM(s, ...) __BXLogM(s, ##__VA_ARGS__)

FOUNDATION_EXTERN void __BXLog(NSString* format, ...);
FOUNDATION_EXTERN void __BXLogM(NSString* format, ...);

#define _BLog(s, ...)   __BLog(s, ##__VA_ARGS__)

#define _BLogM(s, ...)  __BLogM(s, ##__VA_ARGS__)

FOUNDATION_EXTERN void __BLog(NSString* format, ...);
FOUNDATION_EXTERN void __BLogM(NSString* format, ...);