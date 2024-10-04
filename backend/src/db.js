import pg from 'pg'

const { Client } = pg;

export let dbClient;

(async () => {

    dbClient = new Client({
        connectionString: "postgresql://postgres:ukviwEgOAGtCEAVAKUfKIasORlNGKgWc@junction.proxy.rlwy.net:17667/railway",
        // ssl?: any, // passed directly to node.TLSSocket, supports all tls.connect options
        // statement_timeout?: number, // number of milliseconds before a statement in query will time out, default is no timeout
        // query_timeout?: number, // number of milliseconds before a query call will timeout, default is no timeout
        // lock_timeout?: number, // number of milliseconds a query is allowed to be en lock state before it's cancelled due to lock timeout
        // application_name?: string, // The name of the application that created this Client instance
        // connectionTimeoutMillis?: number, // number of milliseconds to wait for connection, default is no timeout
        // idle_in_transaction_session_timeout?: number // number of milliseconds before terminating any session with an open idle transaction, default is no timeout
    });
    await dbClient.connect();

})();



/*
const res = await client.query('SELECT $1::text as message', ['Hello world!'])
console.log(res.rows[0].message) // Hello world!
await client.end()
*/