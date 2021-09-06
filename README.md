# Docker

Change services.volumes in `docker-compose.yml`

## Build

```
docker build -f=../Dockerfile -t cur-work .
```

## Without systemd

```
docker-compose up --abort-on-container-exit
```

### Inspect database

To look at the content of the serber don't `abort-on-container-exit` and use psql or pgAdmin.

- port : 54321
- username : postgres
- password : pass

## With systemd

Change `Service[]`s `docker-compose.yml` position. in `curdow-docker.service`

Run `docker-setup.sh`

```
./docker-setup.sh
```
