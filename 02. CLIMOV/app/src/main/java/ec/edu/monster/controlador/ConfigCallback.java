package ec.edu.monster.controlador;

public interface ConfigCallback<T>{
    void onSuccess(T result);
    void onError(String error);
}
