import { NEWS_URL, sendRequestWithToken } from "C:/Users/justi/source/repos/OnlineBlog/onlineblog.client/src/services/commonService";

export async function getNewsByUser(userId){
    try {
        const allNews = await sendRequestWithToken(`${NEWS_URL}/${userId}`, 'GET');
        return allNews;
    } catch(error) {
        console.error(error);
        return null;
    }
}
