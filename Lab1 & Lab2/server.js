const express = require("express");
const path = require("path");

const app = express();

app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, "public", "index2.html"));
});

app.get('/1', (req, res) => {
    res.sendFile(path.join(__dirname, "public", "index.html"));
});

app.use(express.static("public"));

const PORT = 3000;

app.listen(PORT, () => {
    console.log("Server started");
})