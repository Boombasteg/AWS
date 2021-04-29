using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;

namespace LambdaCallCounterCdk
{
    public class LambdaCallCounterCdkStack : Stack
    {
        internal LambdaCallCounterCdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var myLambdaFunc = new Function(this, "MyFuncHandler", new FunctionProps
            {
                Runtime = Runtime.NODEJS_10_X,
                Code = Code.FromAsset("lambda"),
                Handler = "hello.handler"
            });

            var myLambdaFuncWithCouner = new HitCounter(this, "MyFuncHandlerHitCounter", new HitCounterProps
            {
                Downstream = myLambdaFunc
            });

            new LambdaRestApi(this, "myLambdaRestApi", new LambdaRestApiProps
            {
                // Handler = myLambdaFunc
                Handler = myLambdaFuncWithCouner.Handler
            });
        }
    }
}
