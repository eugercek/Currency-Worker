sudo systemctl stop curdow.timer
sudo cp /home/umut/src/staj/Currency/curdow.service /etc/systemd/system/
sudo cp /home/umut/src/staj/Currency/curdow.timer /etc/systemd/system/
sudo systemctl daemon-reload
sudo systemctl start curdow.timer
