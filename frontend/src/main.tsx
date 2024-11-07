import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.tsx";
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import Seguimiento from "./Seguimiento.tsx";

const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
    },
    {
        path: "/seguimiento/:gameID",
        element: <Seguimiento />,
    }
]);

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router} />
    </StrictMode>,
);
