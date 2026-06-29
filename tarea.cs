namespace EspacioTarea;
public class T {
 public int TareaID { get; set; }
 public string? Descripcion { get; set; }
private int duracion;

public int Duracion
    {
        get {return duracion;} // si alguien desea saber la duración de la tarea, le mostramos a través del get
        set
        {
            if (value >=  10 && value  <= 100) duracion = value; // verifica que el valor ingresado sea entre 10 o 100
        }
    }


}