aws cloudformation deploy --template-file ./WebApplication1/Deployment/cloudformation.yml --stack-name Second
if [$? -eq 0] || [$? -eq 255]; then
    exit 0
else
    exit 1
fi
