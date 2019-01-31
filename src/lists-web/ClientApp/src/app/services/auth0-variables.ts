interface AuthConfig {
    clientID: string;
    domain: string;
    callbackURL: string;
    apiUrl: string;
}

export const AUTH_CONFIG: AuthConfig = {
    clientID: '0hnr61mouYhOMcTdUw1eQmmYrV23rxx4',
    domain: 'docati.eu.auth0.com',
    callbackURL: 'http://localhost:3000/callback',
    apiUrl: 'https://lists.docati.com/api'
};
