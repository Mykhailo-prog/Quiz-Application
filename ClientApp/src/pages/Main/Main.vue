<template src="./Main.html"></template>

<script>
import UserList from "@/components/UserList";
import LoginUser from "@/components/LoginUser";
import RegisterUser from "@/components/RegisterUser";
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Main",
  data() {
    return {
      instance: null,
      navItem: 1,
      Mess: [],
      HistMess: [],
    };
  },
  methods: {
    ...mapActions(["loadUsers", "cleanUsers"]),
    confirmAlert() {
      this.$bvModal.show("confirmAlert");
      this.navItem--;
    },
    push() {
      /*this.instance.publish({
        channel: "my_channel",
        message: "Hello world",
        sendByPost: true,
        storeInHistory: true,
      });*/
      //console.log(this.instance);
      console.log("Push envoked");
    },
    async MessInfo() {
      var res = await this.instance
        .fetchMessages({
          channels: ["my_channel"],
          end: "1667822572790589",
          count: 5,
        })
        .then((resp) => resp.channels.my_channel);
      console.log(res);
      this.HistMess = res;
      console.log(this.HistMess);
    },
  },
  computed: {
    ...mapGetters(["Users"]),
  },
  async mounted() {
    await this.MessInfo();
    this.instance.addListener({
      message: async (m) => {
        console.log(m);
        this.Mess.push(m);
        await this.MessInfo();
      },
      presence: function(p) {
        // handle presence
        var action = p.action; // Can be join, leave, state-change or timeout
        var channelName = p.channel; // The channel for which the message belongs
        var occupancy = p.occupancy; // No. of users connected with the channel
        var state = p.state; // User State
        var channelGroup = p.subscription; //  The channel group or wildcard subscription match (if exists)
        var publishTime = p.timestamp; // Publish timetoken
        var timetoken = p.timetoken; // Current timetoken
        var uuid = p.uuid; // UUIDs of users who are connected with the channel
      },
      status: function(s) {
        var affectedChannelGroups = s.affectedChannelGroups;
        var affectedChannels = s.affectedChannels;
        var category = s.category;
        var operation = s.operation;
      },
    });
    this.instance.subscribe({ channels: ["my_channel"] }, "inst1");
  },
  async created() {
    this.instance = this.$pnGetInstance("inst1");

    this.cleanUsers();
    await this.loadUsers();
    localStorage.removeItem("token");
  },
  components: {
    UserList,
    LoginUser,
    RegisterUser,
  },
};
</script>
<style scoped src="./Main.css"></style>
