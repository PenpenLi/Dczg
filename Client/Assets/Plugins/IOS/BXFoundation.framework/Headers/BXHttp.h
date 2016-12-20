//
//  BXHttp.h
//
#import <Foundation/Foundation.h>

typedef void(^requestResult)(NSError* error, NSData* responseData);

/*
 * response delegate for GET and POST
 */
@protocol BXHttpDelegate <NSObject>
/*
 * The error is nil when request is success.
 */
-(void)requestResult:(NSError*)error responseData:(NSData*)responseData;
@end

/*
 * Basic object
 * Don't initialize this object
 */
@interface BXHttp : NSObject
{
    NSString*           _url;
    NSMutableData*      _mutableData;
}

- (id)initWithURL:(NSString*)url;

/*
 * start http request
 * choose any one.
 */
- (void)startAsynchronousWithDelegate:(id<BXHttpDelegate>)delegate;
- (void)startAsynchronousWithBlock:(requestResult)block;

@end


@interface BXHttpGet : BXHttp
@property (nonatomic, assign) NSUInteger        timeOutSeconds;
@property (nonatomic, strong) NSDictionary*     parameters;
@property (nonatomic, strong) NSString*         extendParameter;
@property (nonatomic, strong) NSDictionary*     headerParameters;
@end

@interface BXHttpPost : BXHttp
@property (nonatomic, assign) NSUInteger        timeOutSeconds;
@property (nonatomic, strong) NSDictionary*     parameters;
@property (nonatomic, strong) NSString*         extendParameter;
@property (nonatomic, strong) NSDictionary*     headerParameters;
@end

