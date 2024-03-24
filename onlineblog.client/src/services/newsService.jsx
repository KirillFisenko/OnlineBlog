import { NEWS_URL, sendRequestWithToken } from "C:/Users/justi/source/repos/OnlineBlog/onlineblog.client/src/services/commonService";

export async function getNewsByUser(userId) {
    const allNews = await sendRequestWithToken(`${NEWS_URL}/${userId}`, 'GET');
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

export async function deleteNews(newId) {    
    await sendRequestWithToken(`${NEWS_URL}/${newId}`, 'DELETE');    
}