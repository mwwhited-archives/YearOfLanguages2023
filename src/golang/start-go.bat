docker run --name golang -it -v %cd%/myapp/:/usr/src/myapp/ -w /usr/src/myapp/ golang:1.21 bash 
docker start golang 