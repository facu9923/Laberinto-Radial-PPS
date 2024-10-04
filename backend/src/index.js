import express from "express";
import { dbClient } from "./db.js";

import cors from "cors";

import multer from "multer";

const upload = multer();

const app = express();

app.use(cors());

const port = process.env.SERVER_PORT || 3000;

app.post("/", upload.none(), async (req, res) => {

    const { nombre, apellido, edad, cantidad_errores, maximo } = req.body;

    await dbClient.query(
        `INSERT INTO registro (nombre, apellido, edad, cantidad_errores, maximo) VALUES ($1, $2, $3, $4, $5)`,
        [nombre, apellido, edad, cantidad_errores, maximo]
    );

    res.send('ok');

});

app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});