from flask import Flask, request, jsonify
from flask_cors import CORS
import pg8000

app = Flask(__name__)
CORS(app)

# URL de conexión
DATABASE_URL = "postgresql://postgres:ukviwEgOAGtCEAVAKUfKIasORlNGKgWc@junction.proxy.rlwy.net:17667/railway"

# Conectar a la base de datos
def connect_db():
    return pg8000.connect(
        user="postgres",
        password="ukviwEgOAGtCEAVAKUfKIasORlNGKgWc",
        host="junction.proxy.rlwy.net",
        port=17667,
        database="railway"
    )

@app.route("/", methods=["POST"])
def handle_post():
    conn = connect_db()
    try:
        # Recibir datos del formulario
        nombre = request.form['nombre']
        apellido = request.form['apellido']
        
        # Validar que los campos no estén vacíos
        if not nombre or not apellido:
            return jsonify({"error": "Nombre y apellido son requeridos."}), 400
        
        edad = int(request.form['edad'])
        cantidad_errores = int(request.form['cantidad_errores'])
        maximo = int(request.form['maximo'])
        
        # Ejecutar la consulta para insertar en la base de datos
        with conn.cursor() as cursor:
            cursor.execute(
                """
                INSERT INTO registro (nombre, apellido, edad, cantidad_errores, maximo)
                VALUES ($1, $2, $3, $4, $5)
                """,
                (nombre, apellido, edad, cantidad_errores, maximo)
            )
            conn.commit()  # Confirmar la transacción
        
        # Responder con 'ok'
        return jsonify({"message": "ok"}), 200

    except ValueError as ve:
        return jsonify({"error": f"Error de conversión: {str(ve)}"}), 400
    except Exception as e:
        return jsonify({"error": str(e)}), 500

    finally:
        conn.close()  # Cerrar la conexión


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=80)
