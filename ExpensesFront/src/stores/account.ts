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
                axiosInstance.post('account/login', {
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
        }
    },
    getters: {
        fullName(): string {
            return `${this.loggedAccount?.firstName} ${this.loggedAccount?.lastName}`
        },
        permissions(): Permission[] {
            return this.loggedAccount ?this.loggedAccount?.permissions :[]
        }
    }
})