import { defineStore } from "pinia";
import axiosInstance from "@/plugins/axios";
import type Account from "@/models/account";
import type Permission from "@/models/permission";


interface AccountState  {
    token: string | null
    loggedAccount: Account | null
}

export const useAccountStore = defineStore('acconts', {
    state: (): AccountState => ({
        token: null,
        loggedAccount: null
    }),
    actions: {
        login(userName: string, password: string): Promise<any> {
            return new Promise((resolve, reject) => {
                axiosInstance.post('accounts/login', {
                    Username: userName,
                    Password: password
                })
                .then(response => {
                    resolve(response.data)
                })
                .catch(() => {
                    reject()
                })
            })
        },
        setAccountState(loggedAccount: Account | null, token: string | null) {
            this.loggedAccount = loggedAccount
            this.token = token
        },
        changePassword(id: string | undefined, oldPassword: string, newPassword: string, repeatNewPassword: string): Promise<any> {
            return new Promise((resolve, reject) => {
                axiosInstance.put('accounts/change_password', null, {params: {
                    Id: id,
                    OldPassword: oldPassword,
                    NewPassword: newPassword,
                    RepeatNewPassword: repeatNewPassword
                }})
                .then(response => {
                    resolve(response.data)
                })
                .catch(() => {
                    reject()
                })
            })
        },
        getAccounts(page: number, pageSize: number) :Promise<Account[]> {
            return new Promise<Account[]>((resolve, reject) => {
                axiosInstance
                .get(`accounts?Page=${page}&PageSize=${pageSize}`)
                .then(response => resolve(response?.data))
                .catch(() => reject())
            })
        },
        deleteAccount(id: string | undefined): Promise<any> {
            return new Promise((resolve, reject) => {
                axiosInstance
                .delete(`accounts/${id}`)
                .then(response => resolve(response?.data))
                .catch(() => reject())
            })
        }
    },
    getters: {
        fullName(): string {
            return `${this.loggedAccount?.firstName} ${this.loggedAccount?.lastName}`
        },
        permissions(): Permission[] {
            return this.loggedAccount ?this.loggedAccount?.permissions :[]
        },
        account(): Account | null {
            return this.loggedAccount
        }
    }
})