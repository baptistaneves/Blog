export class Modal {
    static closeModal(buttonId:string) {
        let button : HTMLElement = document.getElementById(buttonId) as HTMLElement;
        button.click();
    }
}