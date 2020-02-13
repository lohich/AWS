aws cloudformation deploy --template-file ./WebApplication1/Deployment/cloudformation.yml --stack-name Second
echo $?
if [ $? -eq 0 ] || [ $? -eq 255 ]; then
    exit 0
else
    exit $?
fi
