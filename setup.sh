#!/bin/bash

sudo setenforce 0
sudo mkdir -p /srv/curdow
sudo rm -rf /srv/curdow/*
sudo chown umut /srv/curdow
sudo systemctl stop curdow.timer
dotnet publish -c Release -o /srv/curdow
