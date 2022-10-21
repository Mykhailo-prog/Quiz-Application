import axios from "axios";

//TODO: should be moved to .env file
axios.defaults.baseURL = process.env.VUE_APP_BACK_URL;
