# sudo setenforce 0
sudo systemctl stop curdow-docker.timer
sudo systemctl stop curdow-docker.service
sudo cp ./curdow-docker.service /etc/systemd/system/
sudo cp ./curdow-docker.timer /etc/systemd/system/
sudo systemctl daemon-reload
sudo systemctl enable --now curdow-docker.timer
