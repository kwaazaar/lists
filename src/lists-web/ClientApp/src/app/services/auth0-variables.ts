interface AuthConfig {
    clientID: string;
    domain: string;
    callbackPath: string;
    apiUrl: string;
    apiScopes: string;
}

export const AUTH_CONFIG: AuthConfig = {
    clientID: '0hnr61mouYhOMcTdUw1eQmmYrV23rxx4',
    domain: 'docati.eu.auth0.com',
    callbackPath: 'callback',
    apiUrl: 'https://lists.docati.com/api',
    apiScopes: 'read:lists write:lists',
};
