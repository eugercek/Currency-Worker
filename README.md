# What is this?

An internship project.

Exchange rates are downloaded, parsed and saved in database with this program.

Afterwards [a REST server](https://github.com/eugercek/Currency-REST-API) reads and serves the databse.

At the last step [a frontend](https://github.com/eugercek/Currency-Frontend) shows data.

# How to run
## One Time

Change services.volumes in `docker-compose.yml`

```sh
docker-compose up --abort-on-container-exit
```

### Inspect database

To look at the content of the database don't `abort-on-container-exit` and use `psq` or pgAdmin.

- port : 54321
- username : postgres
- password : pass

## With systemd

Change `Service[]`s `docker-compose.yml` position. in `curdow-docker.service`

Run `docker-setup.sh`

```
./docker-setup.sh
```

There are also non-docker way to install but they're abandoned!