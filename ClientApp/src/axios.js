import axios from "axios";

//TODO: should be moved to .env file
axios.defaults.baseURL = process.env.QUIZ_APP_BASE_URL;
