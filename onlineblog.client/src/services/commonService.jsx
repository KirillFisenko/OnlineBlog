export const ACCOUNT_URL = 'account';
export const USERS_URL = 'users';
export const SUBSCRIBE_URL = 'subscribe';
export const LIKES_URL = 'likes';
export const NEWS_URL = 'news';
export const TOKEN_URL = 'token';
export const BASE_URL = 'login';
export const TOKEN_NAME = 'Token';
export const PROFILE_URL = '/profile';
export const LOGIN_URL = '/login';
export const SIGNUP_URL = '/signup';
const ISONLINE_NAME = 'ONLINE';

export async function getToken(login, password) {
    const token = await sendAuthenticatedRequest(TOKEN_URL, 'POST', login, password);
    localStorage.setItem(TOKEN_NAME, token.accessToken);
    localStorage.setItem(ISONLINE_NAME, '1');
    window.location.href = PROFILE_URL;
}

async function sendAuthenticatedRequest(url, method, username, password, data) {
    // Создаем объект заголовков
    var headers = new Headers();

    // Устанавливаем заголовок авторизации
    headers.set('Authorization', 'Basic ' + btoa(username + ':' + password));

    // Устанавливаем тип контента (если есть данные)
    if (data) {
        headers.set('Content-Type', 'application/json');
    }

    // Создаем объект параметров запроса
    var requestOptions = {
        method: method,
        headers: headers,
        body: data ? JSON.stringify(data) : undefined
    };

    // Отправляем запрос с помощью fetch
    var resultFetch = await fetch(url, requestOptions);
    if (resultFetch.ok) {
        // Запрос успешно выполнен
        const result = await resultFetch.json();
        return result
    } else {
        // Произошла ошибка при выполнении запроса
        throw new Error('Ошибка ' + resultFetch.status + ': ' + resultFetch.statusText);
    };
}

export async function sendRequestWithToken(url, method, data, withToken = true) {
    var headers = new Headers();
    if (withToken) {
        const token = localStorage.getItem(TOKEN_NAME);
        headers.set('Authorization', `Bearer ${token}`);
    }

    // Устанавливаем тип контента (если есть данные)
    if (data) {
        headers.set('Content-Type', 'application/json');
    }

    // Создаем объект параметров запроса
    var requestOptions = {
        method: method,
        headers: headers,
        body: data ? JSON.stringify(data) : undefined
    };

    // Отправляем запрос с помощью fetch
    var resultFetch = await fetch(url, requestOptions);
    if (resultFetch.ok) {
        // Запрос успешно выполнен
        const result = await resultFetch.json();
        return result
    } else {
        // Произошла ошибка при выполнении запроса
        errorRequest(resultFetch.status);
    };
}

function errorRequest(status) {
    if (status === 401) {
        // Ошибка авторизации 401
        window.location.href = BASE_URL; // Редирект на базовый URL
        clearStore();
    }
}

export function clearStore() {
    localStorage.clear();
}

export function isUserOnline() {
    if(localStorage.getItem(ISONLINE_NAME) == '1'){
        return true;
    }
    return false;
}