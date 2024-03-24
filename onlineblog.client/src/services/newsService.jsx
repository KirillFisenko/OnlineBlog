import {NEWS_URL, sendRequestWithToken } from "./commonService";

export async function getNewsByUser(userId) {
    const allNews = await sendRequestWithToken(`${NEWS_URL}/${userId}`, 'GET');
    return allNews;
}
export async function getNews() {
    const allNews = await sendRequestWithToken(`${NEWS_URL}`, 'GET');
    return allNews;
}

export async function createNews(newNews) {
    newNews.image = newNews.image.toString()
    const news = await sendRequestWithToken(`${NEWS_URL}`, 'POST', newNews);
    return news;
}

export async function updateNews(newNews) {
    newNews.image = newNews.image.toString()
    const news = await sendRequestWithToken(`${NEWS_URL}`, 'PATCH', newNews);
    return news;
}

export async function deleteNews(newsId) {
    await sendRequestWithToken(`${NEWS_URL}/${newsId}`, 'DELETE');
}