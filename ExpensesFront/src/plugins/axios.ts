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
    }
    toast.add({
        severity: 'error',
        summary: summary,
        detail: detail,
        closable: false,
        life: 3000
    })
}

const axiosInstance = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    headers: {
        'Content-Type': 'application/json'
    }
});

axiosInstance.interceptors.request.use(
    config => {
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

axiosInstance.interceptors.response.use(
    response => {
        return response
    },
    error => {
        showNotificationError(error?.response?.data)
        return Promise.reject(error)
    }
);

export default axiosInstance;