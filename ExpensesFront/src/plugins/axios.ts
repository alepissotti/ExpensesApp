import axios from "axios";
import toast from "./toastService";

const showNotificationError = (response: any): void => {
    let summary = 'Error'
    let detail = ''
    switch (response?.status){
        //Bad request
        case 400:
            summary = `${response?.title} (${response?.status})`
            const errorKeys: string[] = Object.keys(response?.errors)
            
            const details: string[] = errorKeys.map(error => {
                return `${error}: ${response.errors[error]}`
            })
            detail = details.join('\n')
            break
        //Not found
        case 404:
            summary = `${response?.title} (${response?.status})`
            detail = response?.detail
            break
        //Internal server Error:
        case 500:
            summary = `${response?.title} (${response?.status})`
            break
    }
    toast.add({
        severity: 'error',
        summary: summary,
        detail: detail,
        closable: false,
        life: 5000
    })
}

const showNotificationSuccess = (response: any) => {
    let summary = ''
    let detail = ''
    const method = response?.config?.method
    if (method && ['put', 'post', 'delete'].includes(method)) {
        switch (method) {
            case 'put':
                summary = `Registro actualizado exitosamente (${response?.status})`,
                detail = (response?.data?.message) ?response?.data?.message :''
                break
            case 'post':
                summary = `Registro creado exitosamente (${response?.status})`,
                detail = (response?.data?.message) ?response?.data?.message :''
                break
            case 'delete':
                summary = `Registro delete exitosamente (${response?.status})`,
                detail = (response?.data?.message) ?response?.data?.message :''
                break
        }
        toast.add({
            severity: 'success',
            summary: summary,
            detail: detail,
            closable: false,
            life: 5000
        })

    }
}

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    headers: {
        'Content-Type': 'application/json'
    }
});

axiosInstance.interceptors.request.use(
    config => {
        const token = sessionStorage.getItem('expensesAppToken')
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config
    },
    error => {
        return Promise.reject(error);
    }
);

axiosInstance.interceptors.response.use(
    response => {
        showNotificationSuccess(response)
        return response
    },
    error => {
        showNotificationError(error?.response?.data)
        return Promise.reject(error)
    }
);

export default axiosInstance;