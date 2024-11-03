using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static int getSceneID(int cantidad_brazos, string dificultad, string ambiente)
    {
        int sceneBuildIndex = -1;

        if (cantidad_brazos == 6 && dificultad == "facil")
            sceneBuildIndex = 1;
        if (cantidad_brazos == 6 && dificultad == "normal")
            sceneBuildIndex = 2;
        if (cantidad_brazos == 8 && dificultad == "facil")
            sceneBuildIndex = 3;
        if (cantidad_brazos == 8 && dificultad == "normal")
            sceneBuildIndex = 4;
        if (cantidad_brazos == 12 && dificultad == "facil")
            sceneBuildIndex = 5;
        if (cantidad_brazos == 12 && dificultad == "normal")
            sceneBuildIndex = 6;

        if (sceneBuildIndex == -1)
            return -1;

        if (ambiente == "clinica")
            sceneBuildIndex += 6;

        return sceneBuildIndex;
    }
}
