import Vue from "vue";
import Router from "vue-router";
import Main from "./pages/Main";
import Start from "./pages/Start";
import Tests from "./pages/Tests";
import TestResult from "./pages/TestResult";
import ResetPassword from "./pages/ResetPassword";

Vue.use(Router);

export default new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "Main",
      component: Main,
    },
    {
      path: "/quiz",
      name: "Quiz",
      component: Start,
      //TODO: I think we need to check if user authenticaten on vue router level too.
      // meta: {
      //   requiresAuth: true
      // }
      // And check this metha prop in router.beforeEach()
    },
    {
      path: "/tests",
      name: "Tests",
      component: Tests,
    },
    {
      path: "/result",
      name: "Test Result",
      component: TestResult,
    },
    {
      path: "/resetpassword",
      name: "Reset Password",
      component: ResetPassword,
    },
  ],
});
