import "./App.css";

import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "./components/ui/select";
import { Label } from "./components/ui/label";
import { Button } from "./components/ui/button";
import { Toaster } from "./components/ui/toaster"
import { useEffect, useState } from "react";
import URLGenerada from "./components/URLGenerada";
import { randomID } from "./lib/utils";

function App() {

    const [brazos, setBrazos] = useState("8");
    const [dificultad, setDificultad] = useState("normal");
    const [ambiente, setAmbiente] = useState("naturaleza");
    const [id, setId] = useState<string|null>(null);

    const [url, setUrl] = useState<string | null>(null);

    useEffect(() => {
        setUrl(null);
    }, [brazos]);

    function onGenerateLink() {

        const urlSearchParams = new URLSearchParams();
        urlSearchParams.append("brazos", brazos);
        urlSearchParams.append("dificultad", dificultad);
        urlSearchParams.append("ambiente", ambiente);

        setId(randomID(12));

        setUrl("https://experimento.laberinto-radial.tech/?" + urlSearchParams.toString())
    };

    return (
        <div>
            <main className="p-20 flex flex-col items-center">

                <h1 className="text-4xl font-bold">Configurar un experimento</h1>

                <div className="flex flex-col gap-y-5 mt-10 items-center">

                    <div className="flex flex-col gap-y-2">
                        <Label className="text-md">Cantidad de brazos</Label>
                        <Select value={brazos} onValueChange={setBrazos}>
                            <SelectTrigger className="w-[180px]">
                                <SelectValue placeholder="8 brazos" />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="6">6 brazos</SelectItem>
                                <SelectItem value="8">8 brazos</SelectItem>
                                <SelectItem value="12">12 brazos</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="flex flex-col gap-y-2">
                        <Label className="text-md">Dificultad del laberinto</Label>
                        <Select value={dificultad} onValueChange={setDificultad}>
                            <SelectTrigger className="w-[180px]">
                                <SelectValue placeholder="Normal" />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="facil">Facil</SelectItem>
                                <SelectItem value="normal">Normal</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    <div className="flex flex-col gap-y-2">
                        <Label className="text-md">Ambiente</Label>
                        <Select value={ambiente} onValueChange={setAmbiente}>
                            <SelectTrigger className="w-[180px]">
                                <SelectValue placeholder="Naturaleza" />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="naturaleza">Naturaleza</SelectItem>
                                <SelectItem value="clinica">Clínica</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    <Button variant="outline" className="max-w-min" onClick={onGenerateLink}>Generar enlace</Button>
                </div>

                {url && <URLGenerada url={url} id={id || ""} />}

            </main>
            <Toaster />
        </div>

    );
}

export default App;
