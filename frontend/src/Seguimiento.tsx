
import {
    useParams
} from "react-router-dom";

export default function Seguimiento() {

    let { gameID } = useParams();

    return (
        <div>seguimiento de: {gameID}</div>
    )
}
