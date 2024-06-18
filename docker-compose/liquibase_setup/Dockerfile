FROM liquibase/liquibase:4.28.0
COPY ["./lib/mongodb-driver-core-5.1.1.jar", "./lib/mongodb-driver-sync-5.1.1.jar", "./lib/bson-5.1.1.jar", "./lib/liquibase-mongodb-4.28.0.jar", "/liquibase/lib/"]
# USER root
# RUN apt-get update && apt-get install -y gnupg && wget -qO- https://www.mongodb.org/static/pgp/server-7.0.asc | tee /etc/apt/trusted.gpg.d/server-7.0.asc
# RUN touch /etc/apt/sources.list.d/mongodb-org-7.0.list && echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu jammy/mongodb-org/7.0 multiverse" | tee /etc/apt/sources.list.d/mongodb-org-7.0.list
# RUN apt-get update && apt-get install -y mongodb-mongosh
# USER liquibase:liquibase
