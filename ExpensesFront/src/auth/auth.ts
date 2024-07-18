import { jwtDecode } from 'jwt-decode';
import type Account from '@/models/account';
import { getActivePinia } from 'pinia';
import { useAccountStore } from '@/stores/account';


const tokenKey = 'expensesAppToken'


export function isAutenticathed(): Boolean {
    return !!sessionStorage.getItem(tokenKey);
}
export function handleLogin(token: string, router: any): any {
    sessionStorage.setItem(tokenKey, token);
    router.push('/')
}

export function handleRetriveData(): any {
    const pinia = getActivePinia();
    const accountStore = useAccountStore(pinia);
    const token = sessionStorage.getItem(tokenKey);
    if (token) {
        try {
            const jwtDecoded = jwtDecode(token);
            const accountData = JSON.parse(`${jwtDecoded.sub}`)
            if (accountData) {
                const loggedAccount: Account = {
                    id: accountData?.Id,
                    firstName: accountData?.FirstName,
                    lastName: accountData?.LastName,
                    userName: accountData?.UserName,
                    permissions: JSON.parse(JSON.stringify(accountData?.Permissions)),
                    roleId: accountData?.RoleId,
                }
                accountStore.setAccountState(loggedAccount, token)
            }
        }
        catch(error) {
            console.log('Error al recuperar el token:' + error)
        }
    }
}