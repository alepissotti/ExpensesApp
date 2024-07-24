import type MenuItem from "./menuItem";

export default interface MenuModule {
    name: string,
    icon: string | null,
    childrens: MenuItem[]
}