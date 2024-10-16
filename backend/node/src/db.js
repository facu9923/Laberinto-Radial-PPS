import pg from 'pg'

const { Client } = pg;

export let dbClient;

(async () => {

    dbClient = new Client({
        connectionString: "postgresql://postgres:ukviwEgOAGtCEAVAKUfKIasORlNGKgWc@junction.proxy.rlwy.net:17667/railway"
    });
    await dbClient.connect();

})();