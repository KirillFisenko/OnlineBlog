import { sendRequestWithToken } from "./commonService";
import {ACCOUNT_URL} from "./commonService";

export async function getUser(){
    var user = await sendRequestWithToken(ACCOUNT_URL, 'GET')
    return user;
}