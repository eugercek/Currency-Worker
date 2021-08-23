#!/bin/bash

sudo setenforce 0
sudo mkdir -p /srv/curdow
sudo rm -rf /srv/curdow/*
sudo chown umut /srv/curdow
sudo systemctl stop curdow
cd /home/umut/src/staj/Currency/Program
dotnet publish -c Release -o /srv/curdow
