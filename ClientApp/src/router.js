import Vue from "vue";
import Router from "vue-router";
import Main from "./pages/Main";
import Start from "./pages/Start";
import Tests from "./pages/Tests";
import TestResult from "./pages/TestResult";
import ResetPassword from "./pages/ResetPassword";

Vue.use(Router);
let router = new Router({
  mode: "history",
  base: process.env.BASE_URL,
  routes: [
    {
      path: "/",
      name: "Main",
      component: Main,
      meta: { requireAuth: false },
    },
    {
      path: "/quiz",
      name: "Quiz",
      component: Start,
      meta: { requireAuth: true },
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
      meta: { requireAuth: true },
    },
    {
      path: "/result",
      name: "Test Result",
      component: TestResult,
      meta: { requireAuth: true },
    },
    {
      path: "/resetpassword",
      name: "Reset Password",
      component: ResetPassword,
      meta: { requireAuth: false, res: "" },
    },
  ],
});
router.beforeEach((to, from, next) => {
  if (to.matched.some((prop) => prop.meta.requireAuth)) {
    if (localStorage.getItem("token") === null) {
      router.replace("/");
      alert("You are not Authenticated!");
    } else {
      next();
    }
  } else {
    if (to.name != "Main" && to.name != "Reset Password") {
      router.replace("/");
      alert("Wrong Direct!");
    } else {
      next();
    }
  }
});
export default router;
