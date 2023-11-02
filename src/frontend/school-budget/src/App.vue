<template>
  <v-app>
    <v-app-bar>
      <v-app-bar-title>Школьные финансы</v-app-bar-title>
    </v-app-bar>
    <v-navigation-drawer rail expand-on-hover>
      <v-list>
        <v-list-item title="Ученики" to="students" prepend-icon="mdi-account-school" />
        <v-list-item title="Взрослые" to="adults" prepend-icon="mdi-account-tie-woman" />
      </v-list>
    </v-navigation-drawer>
    <v-main>
      <default-view />
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import DefaultView from "./layouts/default/View.vue";
</script>
