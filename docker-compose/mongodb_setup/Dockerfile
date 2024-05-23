FROM mongo:8.0.0-rc4
# Will be set through Environment Files
ARG DATABASE_URI
ARG USERNAME
ARG PASSWORD
ARG DATABASE_NAME

ENV DATABASE_URI ${DATABASE_URI}
ENV USERNAME ${USERNAME}
ENV PASSWORD ${PASSWORD}
ENV DATABASE_NAME ${DATABASE_NAME}

COPY ["./ciclisme/", "/ciclisme/"]

CMD mongoimport --uri ${DATABASE_URI} --db ${DATABASE_NAME} --collection ciclistas --file /ciclisme/ciclistas.json && \
    mongoimport --uri ${DATABASE_URI} --db ${DATABASE_NAME} --collection equipos --file /ciclisme/equipos.json && \
    mongoimport --uri ${DATABASE_URI} --db ${DATABASE_NAME} --collection etapas --file /ciclisme/etapas.json && \
    mongoimport --uri ${DATABASE_URI} --db ${DATABASE_NAME} --collection maillots --file /ciclisme/maillots.json


