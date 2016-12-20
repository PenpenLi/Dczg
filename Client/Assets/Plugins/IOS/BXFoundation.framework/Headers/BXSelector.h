/*
 * BXSelector
 * Store the function and parameters.
 * Run when you want.
 * Max 16 parameters supported;
 */
#import <Foundation/Foundation.h>


@interface BXSelector : NSObject
/*
 * Create a function
 */
+ (instancetype)selector:(SEL)selector target:(id)target;

- (void)addParameter:(id)parameter;
- (void)addIntegerParameter:(NSInteger)parameter;
- (void)addFloatParameter:(float)parameter;

- (void)excuteWithResult:(void*)result;
- (void)excute;
@end

#define BXS_CURRENT_SEL    [BXSelector selector:_cmd target:self]

