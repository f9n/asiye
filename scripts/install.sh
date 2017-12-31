#!/usr/bin/env bash

SETUPPATH="/opt/asiye"

if [ "$UID" != "0" ]; then
	echo "You must be root!"
	exit 1;
fi

which dotnet
if [ "$?" != "0" ]; then
	echo "Please install dotnet cli"
	exit 1;
fi

git clone https://github.com/pleycpl/asiye.git ${SETUPPATH}

dotnet publish ${SETUPPATH}/asiye.csproj

cp ${SETUPPATH}/scripts/asiye.sh /usr/bin/asiye

exit 0;