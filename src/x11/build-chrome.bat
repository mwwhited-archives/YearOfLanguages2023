docker build -t oobdev/google-chrome -f DockerFile.chrome .

docker volume create google-chrome-cache
docker volume create google-chrome-config

docker run --rm -it ^
-v /run/desktop/mnt/host/wslg/.X11-unix:/tmp/.X11-unix ^
-v /run/desktop/mnt/host/wslg:/mnt/wslg ^
-v google-chrome-cache:/root/.cache/google-chrome ^
-v google-chrome-config:/root/.config/google-chrome ^
--gpus all ^
oobdev/google-chrome

REM docker compose -f docker-compose.google-chrome.yaml up


docker run --rm -it ^
-v /run/desktop/mnt/host/wslg/.X11-unix:/tmp/.X11-unix ^
-v /run/desktop/mnt/host/wslg:/mnt/wslg ^
-v google-chrome-cache:/root/.cache/google-chrome ^
-v google-chrome-config:/root/.config/google-chrome ^
--gpus all ^
oobdev/google-chrome bash

@REM -v google-chrome-cache:/home/docker/.cache/google-chrome ^
@REM -v google-chrome-config:/home/docker/.config/google-chrome ^