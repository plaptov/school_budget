<template>
  <Suspense>
    <v-container fluid>
      <v-row>
        <v-col>
          <v-btn prepend-icon="$plus" @click="addFund()"> Добавить </v-btn>
        </v-col>
      </v-row>

      <v-row v-if="editingValue">
        <v-col>
          <v-card>
            <v-card-title>
              <span class="text-h5">Редактирование</span>
            </v-card-title>
            <v-card-text>
              <v-row>
                <v-col>
                  <v-text-field v-model="editingValue.name" label="Название"></v-text-field>
                </v-col>
                <v-col>
                  <v-combobox v-model="editingValue.type" :items="fundTypes" label="Тип"></v-combobox>
                </v-col>
              </v-row>
              <v-row>
                <v-col>
                  <div style="display:flex; gap: 10px">
                    <v-btn color="primary" @click="saveModel()">Сохранить</v-btn>
                    <v-btn color="error" @click="editingValue = null">Отменить</v-btn>
                  </div>
                </v-col>
              </v-row>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>

      <v-row>
        <v-col>
          <v-table>
            <thead>
              <tr>
                <th/>
                <th class="text-left">ID</th>
                <th class="text-left">Название</th>
                <th class="text-left">Тип</th>
                <th class="text-left">Закрыт</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="adult in funds" :key="adult.id">
                <td>
                  <v-icon icon="$edit" @click="editingValue = adult"/>
                  <v-icon icon="$delete" @click="deleteModel(adult)"/>
                </td>
                <td>{{ adult.id }}</td>
                <td>{{ adult.name }}</td>
                <td>{{ adult.type }}</td>
                <td>{{ adult.isClosed }}</td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
      </v-row>
    </v-container>
    <template #fallback> Загрузка... </template>
  </Suspense>
</template>

<script lang="ts" setup>
import { onBeforeMount, ref } from "vue";
import { CrudService } from "@/services/crud-service";
import { Fund, FundType } from "@/models/fund";

const fundTypes = [
  FundType.Default,
  FundType.Targeted,
  FundType.Continuous,
];
const funds = ref<Fund[]>([]);
const service = new CrudService<Fund>("fund");
const editingValue = ref<Fund | null>(null);

onBeforeMount(async () => {
  funds.value = await service.getAll();
});

function addFund(): void {
  editingValue.value = {
    id: 0,
    name: "",
    isClosed: false,
    canClose: true,
    type: FundType.Targeted,
  }
}

async function saveModel(): Promise<void> {
  const adult = editingValue.value!;
  if (adult.id) {
    await service.update(adult);
  } else {
    const newAdult = await service.create(adult);
    funds.value.push(newAdult);
  }
  editingValue.value = null;
}

async function deleteModel(model: Fund): Promise<void> {
  await service.delete(model.id);
  funds.value = funds.value.filter((s) => s.id !== model.id);
}
</script>
