import { FaRegCopy } from "react-icons/fa";
import { useRef } from "react";
import { useToast } from "../components/ui/use-toast";

export default function URLGenerada({ url, id }: { url: string, id: string }) {

    const inputRef = useRef<HTMLInputElement>(null);
    const { toast } = useToast();

    return (
        <>
            <div
                className="rounded-lg border-gray-200 border-2 p-3 text-gray-400 flex items-center mt-5"
                onClick={async () => {
                    if (!inputRef.current) return;
                    inputRef.current.focus();
                    inputRef.current.select();
                    toast({
                        description: "Texto copiado al portapapeles"
                    })
                    await navigator.clipboard.writeText(inputRef.current.value);
                }}
            >
                <input type="text" value={url} readOnly ref={inputRef} className="focus:outline-none" />
                <FaRegCopy className="cursor-pointer ml-2" />
            </div>

            <div>
                <a href={`/seguimiento?id=${id}`} target="_blank" className="mt-2 font-semibold underline text-gray-600 text-sm">Seguimiento</a>
            </div>
        </>
    );
}