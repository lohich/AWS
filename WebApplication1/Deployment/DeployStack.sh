aws cloudformation deploy --template-file ./WebApplication1/Deployment/cloudformation.yml --stack-name Books --parameter-overrides EC2AccessKey=Admin --capabilities CAPABILITY_IAM
echo $?
if [ $? -eq 0 ] || [ $? -eq 255 ]; then
    exit 0
else
    exit $?
fi
