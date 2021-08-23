#!/bin/bash
mkdir -p ~/.local/bin

cd ./Program
dotnet publish -c Release -o ~/.local/bin
cd ..

sudo cp ./currency-downloader.service /etc/systemd/system/
sudo cp ./currency-downloader.timer /etc/systemd/system/

sudo systemctl daemon-reload

sudo setenforce 0

sudo systemctl start currency-downloader.timer
