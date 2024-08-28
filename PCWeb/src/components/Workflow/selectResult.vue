<template>
	<div class="select-result l">
		<p class="clear">
			已选（{{ total }}）
			<a @click="emits('del')">清空</a>
		</p>
		<ul>
			<template v-for="{ type, data, cancel } in list" :key="type">
				<template v-if="type === 'role'">
					<li v-for="item in data" :key="item.id">
						<img src="@/assets/images/icon_role.png" />
						<span>{{ item.name }}</span>
						<img src="@/assets/images/cancel.png" @click="cancel(item)" />
					</li>
				</template>
				<template v-if="type === 'department'">
					<li v-for="item in data" :key="item.id">
						<img src="@/assets/images/icon_file.png" />
						<span>{{ item.name }}</span>
						<img src="@/assets/images/cancel.png" @click="cancel(item)" />
					</li>
				</template>
				<template v-if="type === 'employee'">
					<li v-for="item in data" :key="item.id">
						<img src="@/assets/images/icon_people.png" />
						<span>{{ item.name }}</span>
						<img src="@/assets/images/cancel.png" @click="cancel(item)" />
					</li>
				</template>
			</template>
		</ul>
	</div>
</template>
<script lang="ts" setup>
defineProps({
	total: {
		type: Number,
		default: 0,
	},
	list: {
		type: Array<any>,
		default: () => [],
	},
});
let emits = defineEmits(['del']);
</script>

<style lang="scss" scoped>
.select-result {
	width: 276px;
	height: 100%;
	font-size: 12px;

	ul {
		height: 460px;
		overflow-y: auto;

		li {
			margin: 11px 26px 13px 19px;
			line-height: 17px;

			span {
				vertical-align: middle;
			}

			img {
				&:first-of-type {
					width: 14px;
					vertical-align: middle;
					margin-right: 5px;
				}

				&:last-of-type {
					float: right;
					margin-top: 2px;
					width: 14px;
				}
			}
		}
	}

	p {
		padding-left: 19px;
		padding-right: 20px;
		line-height: 37px;
		border-bottom: 1px solid #f2f2f2;

		a {
			float: right;
		}
	}
}
</style>
