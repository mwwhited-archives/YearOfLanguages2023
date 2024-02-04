# Kubernetes

## Notes

* https://www.composerize.com/
* https://github.com/dockersamples
* https://www.middlewareinventory.com/blog/deploy-docker-image-to-kubernetes/

## Scripts

```shell
docker run -d --rm --name web-test -p 80:8000 crccheck/hello-world
```

```shell
docker build -f hello-world.DockerFile -t mwwhited/hello-world .
docker run -d --rm --name mwwhited-hello -p 80:8000 mwwhited/hello-world

docker-compose --file .\hello-world.yml up
```
