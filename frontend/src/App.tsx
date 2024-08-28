import "./App.css";

import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import { Toaster } from "@/components/ui/toaster"
import { useEffect, useState } from "react";
import URLGenerada from "@/components/URLGenerada";

function App() {

    const [brazos, setBrazos] = useState("8");
    const [url, setUrl] = useState<string | null>(null);

    useEffect(() => {
        setUrl(null);
    }, [brazos]);

    function onGenerateLink() {

        console.log("Cantidad de brazos: " + brazos);

        const urlSearchParams = new URLSearchParams();
        urlSearchParams.append("brazos", brazos);

        setUrl(window.location.href + "?" + urlSearchParams.toString())
    };

    return (
        <div>
            <main className="p-20 flex flex-col items-center">

                <h1 className="text-4xl font-bold">Configurar un experimento</h1>

                <div className="flex flex-col gap-y-5 mt-10 items-center">

                    <div className="flex flex-col gap-y-2">
                        <Label className="text-md">Brazos del laberinto</Label>
                        <Select value={brazos} onValueChange={setBrazos}>
                            <SelectTrigger className="w-[180px]">
                                <SelectValue placeholder="8 brazos" />
                            </SelectTrigger>
                            <SelectContent>
                                <SelectItem value="4">4 brazos</SelectItem>
                                <SelectItem value="8">8 brazos</SelectItem>
                                <SelectItem value="16">16 brazos</SelectItem>
                            </SelectContent>
                        </Select>
                    </div>

                    <Button variant="outline" className="max-w-min" onClick={onGenerateLink}>Generar enlace</Button>
                </div>

                {url && <URLGenerada url={url} />}

            </main>
            <Toaster />
        </div>

    );
}

export default App;
