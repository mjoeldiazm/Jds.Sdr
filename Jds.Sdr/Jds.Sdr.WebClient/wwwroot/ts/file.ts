namespace JSInteropWithTypeScript {

    class ExampleJsFunctions {
        public showPrompt(message: string): string {
            return prompt(message, 'Type anything here');
        }
    }

    export function Load(): void {
        window['exampleJsFunctions'] = new ExampleJsFunctions();
    }
}

JSInteropWithTypeScript.Load();