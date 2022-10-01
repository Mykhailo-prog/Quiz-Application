import axios from "axios";

axios.defaults.baseURL = "https://localhost:44378/api/";
axios.defaults.headers.Authorization =
  "Bearer " + localStorage.getItem("token");
